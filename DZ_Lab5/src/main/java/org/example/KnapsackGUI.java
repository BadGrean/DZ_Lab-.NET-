package org.example;

import javax.swing.*;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

public class KnapsackGUI extends JFrame {
    private JTextField nField;
    private JTextField seedField;
    private JTextField lowerBoundField;
    private JTextField upperBoundField;
    private JTextField capacityField;
    private JTextArea itemsArea;
    private JTextArea resultArea;
    private JButton solveButton;

    public KnapsackGUI() {
        setTitle("Knapsack Problem Solver");
        setSize(600, 600);
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        setLocationRelativeTo(null);

        // Setting up the layout
        setLayout(new BorderLayout());

        // Input Panel
        JPanel inputPanel = new JPanel();
        inputPanel.setLayout(new GridLayout(6, 2));

        // Adding input fields
        inputPanel.add(new JLabel("Number of item types:"));
        nField = new JTextField();
        inputPanel.add(nField);

        inputPanel.add(new JLabel("Seed:"));
        seedField = new JTextField();
        inputPanel.add(seedField);

        inputPanel.add(new JLabel("Lower bound for value and weight:"));
        lowerBoundField = new JTextField();
        inputPanel.add(lowerBoundField);

        inputPanel.add(new JLabel("Upper bound for value and weight:"));
        upperBoundField = new JTextField();
        inputPanel.add(upperBoundField);

        inputPanel.add(new JLabel("Knapsack capacity:"));
        capacityField = new JTextField();
        inputPanel.add(capacityField);

        // Solve button
        solveButton = new JButton("Solve");
        inputPanel.add(solveButton);

        add(inputPanel, BorderLayout.NORTH);

        // Text Areas
        itemsArea = new JTextArea(10, 30);
        itemsArea.setEditable(false);
        resultArea = new JTextArea(10, 30);
        resultArea.setEditable(false);

        // Scroll panes for text areas
        JScrollPane itemsScrollPane = new JScrollPane(itemsArea);
        JScrollPane resultScrollPane = new JScrollPane(resultArea);

        // Adding text areas to the frame
        add(itemsScrollPane, BorderLayout.CENTER);
        add(resultScrollPane, BorderLayout.SOUTH);

        // Adding action listener to the button
        solveButton.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                solveKnapsackProblem();
            }
        });
    }

    private void solveKnapsackProblem() {
        try {
            int n = Integer.parseInt(nField.getText());
            long seed = Long.parseLong(seedField.getText());
            int lowerBound = Integer.parseInt(lowerBoundField.getText());
            int upperBound = Integer.parseInt(upperBoundField.getText());
            int capacity = Integer.parseInt(capacityField.getText());

            // Creating the problem instance
            Problem problem = new Problem(n, seed, lowerBound, upperBound);

            // Displaying generated items
            itemsArea.setText(problem.toString());

            // Solving the knapsack problem
            Result result = problem.solve(capacity);

            // Displaying the result
            resultArea.setText("Result:\n" + result.toString());

        } catch (NumberFormatException ex) {
            JOptionPane.showMessageDialog(this, "Please enter valid numbers in all fields", "Error", JOptionPane.ERROR_MESSAGE);
        }
    }

    public static void main(String[] args) {
        SwingUtilities.invokeLater(new Runnable() {
            @Override
            public void run() {
                new KnapsackGUI().setVisible(true);
            }
        });
    }
}

