package org.example;

import java.util.List;

public class Result {
    List<Integer> selectedItems;
    int totalValue;
    int totalWeight;

    public Result(List<Integer> selectedItems, int totalValue, int totalWeight) {
        this.selectedItems = selectedItems;
        this.totalValue = totalValue;
        this.totalWeight = totalWeight;
    }

    @Override
    public String toString() {
        return "Result{" +
                "selectedItems=" + selectedItems +
                ", totalValue=" + totalValue +
                ", totalWeight=" + totalWeight +
                '}';
    }
}
