package org.example;

import javax.swing.*;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.KeyEvent;
import java.awt.event.KeyListener;
import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.CopyOnWriteArrayList;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;

public class GameBoard extends JPanel implements ActionListener {

    private final int BOARD_WIDTH = 400;
    private final int BOARD_HEIGHT = 400;
    private static final int DOT_SIZE = 10;

    private ArrayList<Point> snake;
    private List<Food> foodItems;
    private Direction direction;
    private Timer timer;
    private boolean inGame;
    private int score;
    private ExecutorService executorService;

    public static int getDotSize() {
        return DOT_SIZE;
    }

    public GameBoard() {
        setPreferredSize(new Dimension(BOARD_WIDTH, BOARD_HEIGHT));
        setBackground(Color.BLACK);
        setFocusable(true);

        executorService = Executors.newCachedThreadPool();

        snake = new ArrayList<>();
        snake.add(new Point(50, 50));
        direction = Direction.RIGHT;
        foodItems = new CopyOnWriteArrayList<>();
        spawnFood();

        //place to initial food spawns

        timer = new Timer(100, this);
        timer.start();

        addKeyListener(new KeyAdapter());
        inGame = true;
        score = 0;
    }

    public void spawnFood() {
        Food food = new Food(BOARD_WIDTH, BOARD_HEIGHT);
        foodItems.add(food);
        executorService.submit(food);
    }

    public List<Food> getFoodItems() {
        return foodItems;
    }

    @Override
    protected void paintComponent(Graphics g) {
        super.paintComponent(g);
        if (inGame) {
            // Draw food
            g.setColor(Color.RED);
            for (Food food : foodItems) {
                Point foodPosition = food.getPosition();
                g.fillRect(foodPosition.x, foodPosition.y, DOT_SIZE, DOT_SIZE);
            }

            // Draw snake
            g.setColor(Color.GREEN);
            for (Point point : snake) {
                g.fillRect(point.x, point.y, DOT_SIZE, DOT_SIZE);
            }

            // Draw score
            String scoreText = "Score: " + score;
            g.setColor(Color.WHITE);
            g.drawString(scoreText, BOARD_WIDTH - 80, BOARD_HEIGHT - 20);
        } else {
            gameOver(g);
        }
    }

    private void gameOver(Graphics g) {
        String msg = "Game Over!";
        Font font = new Font("Helvetica", Font.BOLD, 30);
        FontMetrics metrics = getFontMetrics(font);

        g.setColor(Color.WHITE);
        g.setFont(font);
        g.drawString(msg, (BOARD_WIDTH - metrics.stringWidth(msg)) / 2, BOARD_HEIGHT / 2);
    }

    @Override
    public void actionPerformed(ActionEvent e) {
        if (inGame) {
            move();
            checkCollision();
            checkFoodCollision();
            repaint();
        }
    }

    private void move() {
        Point head = snake.get(0);
        Point newHead = new Point(head.x, head.y);
        switch (direction) {
            case UP:
                newHead.y -= DOT_SIZE;
                break;
            case DOWN:
                newHead.y += DOT_SIZE;
                break;
            case LEFT:
                newHead.x -= DOT_SIZE;
                break;
            case RIGHT:
                newHead.x += DOT_SIZE;
                break;
        }

        snake.add(0, newHead);

        if (foodItems.stream().anyMatch(food -> newHead.equals(food.getPosition()))) {
            spawnFood();
            score++;
        } else {
            snake.remove(snake.size() - 1);
        }
    }

    private void checkCollision() {
        Point head = snake.get(0);
        if (head.x < 0 || head.x >= BOARD_WIDTH || head.y < 0 || head.y >= BOARD_HEIGHT) {
            inGame = false;
        }
        for (int i = 1; i < snake.size(); i++) {
            if (head.equals(snake.get(i))) {
                inGame = false;
                break;
            }
        }
        if (!inGame) {
            timer.stop();
            foodItems.forEach(Food::stop); // Stop all food threads
            executorService.shutdownNow(); // Shutdown the executor service
        }
    }

    private void checkFoodCollision() {
        Point head = snake.get(0);
        foodItems.removeIf(food -> head.equals(food.getPosition()));
    }

    private class KeyAdapter implements KeyListener {
        @Override
        public void keyPressed(KeyEvent e) {
            switch (e.getKeyCode()) {
                case KeyEvent.VK_UP:
                    if (direction != Direction.DOWN) {
                        direction = Direction.UP;
                    }
                    break;
                case KeyEvent.VK_DOWN:
                    if (direction != Direction.UP) {
                        direction = Direction.DOWN;
                    }
                    break;
                case KeyEvent.VK_LEFT:
                    if (direction != Direction.RIGHT) {
                        direction = Direction.LEFT;
                    }
                    break;
                case KeyEvent.VK_RIGHT:
                    if (direction != Direction.LEFT) {
                        direction = Direction.RIGHT;
                    }
                    break;
            }
        }

        @Override
        public void keyTyped(KeyEvent e) {}

        @Override
        public void keyReleased(KeyEvent e) {}
    }
}
