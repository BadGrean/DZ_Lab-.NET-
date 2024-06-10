package org.example;

import java.util.Scanner;

public class Main {
    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);

        // Pobranie danych wejściowych od użytkownika
        System.out.print("Podaj liczbę rodzajów przedmiotów: ");
        int n = scanner.nextInt();

        System.out.print("Podaj ziarno losowania: ");
        long seed = scanner.nextLong();

        System.out.print("Podaj dolny zakres wartości i wagi przedmiotu: ");
        int lowerBound = scanner.nextInt();

        System.out.print("Podaj górny zakres wartości i wagi przedmiotu: ");
        int upperBound = scanner.nextInt();

        System.out.print("Podaj maksymalną wagę plecaka: ");
        int capacity = scanner.nextInt();

        // Utworzenie instancji problemu plecakowego
        Problem problem = new Problem(n, seed, lowerBound, upperBound);

        // Wyświetlenie wygenerowanych przedmiotów
        System.out.println(problem);

        // Rozwiązanie problemu plecakowego
        Result result = problem.solve(capacity);

        // Wyświetlenie wyniku
        System.out.println("Wynik:");
        System.out.println(result);

        scanner.close();
    }
}
