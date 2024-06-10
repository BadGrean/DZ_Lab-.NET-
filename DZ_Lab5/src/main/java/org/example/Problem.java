package org.example;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;
import java.util.Random;

public class Problem {
    int n;
    long seed;
    int lowerBound;
    int upperBound;
    List<Przedmiot> items;

    public Problem(int n, long seed, int lowerBound, int upperBound) {
        this.n = n;
        this.seed = seed;
        this.lowerBound = lowerBound;
        this.upperBound = upperBound;
        this.items = new ArrayList<>();
        generateItems();
    }

    private void generateItems() {
        Random random = new Random(seed);
        for (int i = 0; i < n; i++) {
            int value = random.nextInt(upperBound - lowerBound + 1) + lowerBound;
            int weight = random.nextInt(upperBound - lowerBound + 1) + lowerBound;
            items.add(new Przedmiot(value, weight));
        }
    }

    public Result solve(int capacity) {
        // Sortowanie przedmiotów po stosunku wartości do wagi malejąco
        List<Przedmiot> sortedItems = new ArrayList<>(items);
        Collections.sort(sortedItems, (a, b) -> Double.compare((double) b.value / b.weight, (double) a.value / a.weight));

        List<Integer> selectedItems = new ArrayList<>();
        int totalValue = 0;
        int totalWeight = 0;

        for (Przedmiot item : sortedItems) {
            while (totalWeight + item.weight <= capacity) {
                totalWeight += item.weight;
                totalValue += item.value;
                selectedItems.add(items.indexOf(item) + 1); // Dodanie indeksu przedmiotu (1-based)
            }
        }

        return new Result(selectedItems, totalValue, totalWeight);
    }

    @Override
    public String toString() {
        StringBuilder sb = new StringBuilder();
        sb.append("Problem: \n");
        for (int i = 0; i < items.size(); i++) {
            sb.append("Przedmiot ").append(i + 1).append(": ").append(items.get(i)).append("\n");
        }
        return sb.toString();
    }
}
