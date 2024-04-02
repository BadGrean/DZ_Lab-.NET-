using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DZ_Lab2
{
    public partial class Form1 : Form
    {
        private string ApiKey;
        private string RequestUri;

        public Form1()
        {
            InitializeComponent();
            ApiKey = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ApiKey.txt")).Trim();
            RequestUri = "https://openexchangerates.org/api/latest.json?app_id=" + ApiKey;
            LoadRatesAsync();
        }
        private async void Form1_Load(object sender, EventArgs e)
        {
            await PopulateTimestampsListBox();
        }

        private double DateTimeToUnixTimeStamp(DateTime dateTime)
        {
            // Round down to the nearest hour
            DateTime roundedDateTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, 0, 0, 0, dateTime.Kind);

            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return (roundedDateTime.ToUniversalTime() - epoch).TotalSeconds;
        }
        private double DateTimeToUnixTimestampSeconds(DateTime dateTime)
        {
            DateTime utcDateTime = dateTime.ToUniversalTime();
            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            double unixTimestamp = (utcDateTime - epoch).TotalSeconds;
            return unixTimestamp;
        }

        private async void LoadRatesAsync()
        {
            try
            {
                double currentTimestamp = DateTimeToUnixTimeStamp(DateTime.UtcNow);

                using (var context = new ApplicationDbContext())
                {
                    double lowerBound = currentTimestamp - 60; // 60 seconds before
                    double upperBound = currentTimestamp + 60; // 60 seconds after

                    var matchingRecord = await context.ExchangeRateRecords
                                    .FirstOrDefaultAsync(x => x.Id >= lowerBound && x.Id <= upperBound);

                    if (matchingRecord != null)
                    {
                        MessageBox.Show($"Loading rates from database for timestamp: {matchingRecord.Id}.");

                        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                        var rates = JsonSerializer.Deserialize<Dictionary<string, decimal>>(matchingRecord.RatesJson, options);

                        textBoxRates.Clear();
                        textBoxRates.AppendText($"Base: {matchingRecord.BaseCurrency}\r\n");
                        foreach (var rate in rates)
                        {
                            textBoxRates.AppendText($"{rate.Key}: {rate.Value}\r\n");
                        }
                        return; // Skip the HTTP request
                    }
                }
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(RequestUri);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();

                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    var result = JsonSerializer.Deserialize<ExchangeRates>(responseBody, options);

                    if (result != null && result.Rates != null)
                    {
                        textBoxRates.Clear();

                        textBoxRates.AppendText($"Base: {result.Base}\r\n");
                        foreach (var rate in result.Rates)
                        {
                            textBoxRates.AppendText($"{rate.Key}: {rate.Value}\r\n");
                        }

                        using (var context = new ApplicationDbContext())
                        {
                            var record = new ExchangeRateRecord
                            {
                                Id = result.Timestamp,
                                BaseCurrency = result.Base,
                                Date = UnixTimeStampToDateTime(result.Timestamp),
                                RatesJson = JsonSerializer.Serialize(result.Rates, options)
                            };

                            context.Add(record);
                            await context.SaveChangesAsync();
                        }
                    }
                }
            }
            catch (DbUpdateException dbEx)
            {
                MessageBox.Show($"Database update error: {dbEx.InnerException?.Message}");
            }
            catch (HttpRequestException httpEx)
            {
                MessageBox.Show($"Error fetching data: {httpEx.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}");
            }
        }

        private DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }

        public class ExchangeRates
        {
            public string @Base { get; set; }
            public double Timestamp { get; set; }
            public Dictionary<string, decimal> Rates { get; set; }
        }

        private void textBoxRates_TextChanged(object sender, EventArgs e)
        {

        }

        public class ApplicationDbContext : DbContext
        {
            public DbSet<ExchangeRateRecord> ExchangeRateRecords { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder options)
            {
                var dbPath = $"{AppDomain.CurrentDomain.BaseDirectory}currencyRates.db";
                Console.WriteLine($"Using database path: {dbPath}");
                options.UseSqlite($"Data Source={dbPath}");
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<ExchangeRateRecord>()
                    .Property(e => e.RatesJson)
                    .HasColumnType("TEXT");
            }
        }

        public class ExchangeRateRecord
        {
            public double Id { get; set; }
            public string BaseCurrency { get; set; }
            public DateTime Date { get; set; }
            public string RatesJson { get; set; }
        }

        private async Task PopulateTimestampsListBox()
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var unixTimestamps = await context.ExchangeRateRecords
                        .OrderByDescending(x => x.Id)
                        .Select(x => x.Id)
                        .ToListAsync();

                    Date_list.Items.Clear();

                    foreach (var unixTimestamp in unixTimestamps)
                    {
                        var dateTime = UnixTimeStampToDateTime(unixTimestamp);
                        Date_list.Items.Add(dateTime.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while populating the ListBox: {ex.Message}");
            }
        }

        private async void Date_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Date_list.SelectedItem != null)
            {
                try
                {
                    DateTime selectedDate = DateTime.Parse(Date_list.SelectedItem.ToString());

                    double selectedTimestamp = DateTimeToUnixTimestampSeconds(selectedDate);

                    using (var context = new ApplicationDbContext())
                    {
                        var matchingRecord = await context.ExchangeRateRecords
                                                .FirstOrDefaultAsync(x => x.Id == selectedTimestamp);

                        if (matchingRecord != null)
                        {
                            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                            var rates = JsonSerializer.Deserialize<Dictionary<string, decimal>>(matchingRecord.RatesJson, options);

                            Currency_to_exchange.Items.Clear();
                            Exchanged_currency.Items.Clear();


                            if (rates != null)
                            {
                                foreach (var currency in rates.Keys)
                                {
                                    Currency_to_exchange.Items.Add(currency);
                                    Exchanged_currency.Items.Add(currency);
                                }
                            }

                            textBoxRates.Clear();
                            textBoxRates.AppendText($"Base: {matchingRecord.BaseCurrency}\r\n");
                            foreach (var rate in rates)
                            {
                                textBoxRates.AppendText($"{rate.Key}: {rate.Value}\r\n");
                            }
                        }
                        else
                        {
                            textBoxRates.Text = "No exchange rates found for the selected date.";
                            Currency_to_exchange.Items.Clear();
                            Exchanged_currency.Items.Clear();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
            }
        }

        private void LoadData_Click(object sender, EventArgs e)
        {
            LoadRatesAsync();
        }

        private void Exchanged_currency_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Currency_to_exchange_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Exchange_from_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private async Task<Dictionary<string, decimal>> GetExchangeRatesForDate(DateTime date)
        {
            using (var context = new ApplicationDbContext())
            {
                double dateTimestamp = DateTimeToUnixTimestampSeconds(date.Date);

                var record = await context.ExchangeRateRecords
                                          .FirstOrDefaultAsync(x => x.Id == dateTimestamp);

                if (record == null)
                {
                    return null;
                }
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var rates = JsonSerializer.Deserialize<Dictionary<string, decimal>>(record.RatesJson, options);

                return rates;
            }
        }

        private async void Calculate_exchange_Click(object sender, EventArgs e)
        {
            if (Date_list.SelectedItem == null || Currency_to_exchange.SelectedItem == null || Exchanged_currency.SelectedItem == null)
            {
                MessageBox.Show("Please select a date and both currencies.");
                return;
            }

            DateTime selectedDate = DateTime.Parse(Date_list.SelectedItem.ToString());
            string fromCurrency = Currency_to_exchange.SelectedItem.ToString();
            string toCurrency = Exchanged_currency.SelectedItem.ToString();

            try
            {
                var rates = await GetExchangeRatesForDate(selectedDate);

                if (rates == null || !rates.ContainsKey(fromCurrency) || !rates.ContainsKey(toCurrency))
                {
                    MessageBox.Show("Exchange rate data is missing for the selected date or currencies.");
                    return;
                }

                if (!decimal.TryParse(Exchange_from.Text, out decimal amount))
                {
                    MessageBox.Show("Please enter a valid amount to convert.");
                    return;
                }

                decimal rateFromBase = rates[fromCurrency];
                decimal rateToBase = rates[toCurrency];
                decimal result = (amount / rateFromBase) * rateToBase;

                Exchange_to.Clear();
                Exchange_to.Text = result.ToString("N2"); // Format to 2 decimal places
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void Exchange_to_TextChanged(object sender, EventArgs e)
        {

        }
    }



}

