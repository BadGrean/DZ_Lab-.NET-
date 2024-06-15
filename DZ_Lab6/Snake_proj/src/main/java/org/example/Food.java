package org.example;

import java.awt.Point;
import java.util.Random;

public class Food implements Runnable {
    private Point position;
    private int boardWidth;
    private int boardHeight;
    private static final int DOT_SIZE = GameBoard.getDotSize();
    private boolean running;
    private Random random;

    public Food(int boardWidth, int boardHeight) {
        this.boardWidth = boardWidth;
        this.boardHeight = boardHeight;
        this.random = new Random();
        spawnFood();
        this.running = true;
    }

    public Point getPosition() {
        return position;
    }

    public void spawnFood() {
        int x = random.nextInt(boardWidth / DOT_SIZE) * DOT_SIZE;
        int y = random.nextInt(boardHeight / DOT_SIZE) * DOT_SIZE;
        position = new Point(x, y);
    }

    public void moveRandomly() {
        int direction = random.nextInt(4);
        switch (direction) {
            case 0: // UP
                position.y -= DOT_SIZE;
                break;
            case 1: // DOWN
                position.y += DOT_SIZE;
                break;
            case 2: // LEFT
                position.x -= DOT_SIZE;
                break;
            case 3: // RIGHT
                position.x += DOT_SIZE;
                break;
        }

        // Ensure the food stays within the board boundaries
        if (position.x < 0) position.x = 0;
        if (position.x >= boardWidth) position.x = boardWidth - DOT_SIZE;
        if (position.y < 0) position.y = 0;
        if (position.y >= boardHeight) position.y = boardHeight - DOT_SIZE;
    }

    @Override
    public void run() {
        while (running) {
            try {
                Thread.sleep(200); // Moves at half the speed of the snake (100 ms)
                moveRandomly();
            } catch (InterruptedException e) {
                Thread.currentThread().interrupt();
            }
        }
    }

    public void stop() {
        running = false;
    }
}
