package org.example;

public class Przedmiot {
    int value;
    int weight;

    public Przedmiot(int value, int weight) {
        this.value = value;
        this.weight = weight;
    }

    @Override
    public String toString() {
        return "Przedmiot{" +
                "value=" + value +
                ", weight=" + weight +
                '}';
    }
}
