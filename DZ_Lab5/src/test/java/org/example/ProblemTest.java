package org.example;

import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.*;

import java.util.List;

public class ProblemTest {

    @Test
    public void testAtLeastOneItemSelectedWhenOneMeetsConstraints() {
        Problem problem = new Problem(10, 15, 1, 10);
        Result result = problem.solve(15);

        assertFalse(result.selectedItems.isEmpty(), "Result should contain at least one item if one meets constraints");
    }

    @Test
    public void testNoItemsSelectedWhenNoneMeetConstraints() {
        Problem problem = new Problem(10, 15, 20, 30); // Setting high bounds to ensure no item can fit in the capacity
        Result result = problem.solve(15);

        assertTrue(result.selectedItems.isEmpty(), "Result should be empty if no items meet constraints");
    }

    @Test
    public void testItemsWithinBounds() {
        int lowerBound = 1;
        int upperBound = 10;
        Problem problem = new Problem(10, 15, lowerBound, upperBound);

        for (Przedmiot item : problem.items) {
            assertTrue(item.value >= lowerBound && item.value <= upperBound, "Item value should be within bounds");
            assertTrue(item.weight >= lowerBound && item.weight <= upperBound, "Item weight should be within bounds");
        }
    }

    @Test
    public void testCorrectnessForSpecificInstance() {
        Problem problem = new Problem(10, 15, 5, 10);
        Result result = problem.solve(15);

        int expectedTotalWeight = 14;
        int expectedTotalValue = 20;
        int actualTotalWeight = result.totalWeight;
        int actualTotalValue = result.totalValue;

        assertEquals(expectedTotalWeight, actualTotalWeight, "Total weight should be correct");
        assertEquals(expectedTotalValue, actualTotalValue, "Total value should be correct");
    }
}