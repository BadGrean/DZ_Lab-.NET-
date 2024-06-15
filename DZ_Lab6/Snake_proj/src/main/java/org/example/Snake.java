package org.example;

import java.awt.Point;
import java.util.ArrayList;
import java.util.List;

public class Snake {
    private List<Point> snake;
    private Direction direction;
    private GameBoard gameBoard;

    public Snake(GameBoard gameBoard) {
        this.gameBoard = gameBoard;
        snake = new ArrayList<>();
        snake.add(new Point(50, 50));
        direction = Direction.RIGHT;
    }

    public List<Point> getSnake() {
        return snake;
    }

    public void setDirection(Direction direction) {
        this.direction = direction;
    }

    public void move() {
        Point head = snake.get(0);
        Point newHead = new Point(head.x, head.y);
        switch (direction) {
            case UP:
                newHead.y -= gameBoard.getDotSize();
                break;
            case DOWN:
                newHead.y += gameBoard.getDotSize();
                break;
            case LEFT:
                newHead.x -= gameBoard.getDotSize();
                break;
            case RIGHT:
                newHead.x += gameBoard.getDotSize();
                break;
        }

        snake.add(0, newHead);

        if (gameBoard.getFoodItems().stream().anyMatch(food -> newHead.equals(food.getPosition()))) {
            gameBoard.spawnFood();
        } else {
            snake.remove(snake.size() - 1);
        }
    }

    public boolean checkCollision() {
        Point head = snake.get(0);
        for (int i = 1; i < snake.size(); i++) {
            if (head.equals(snake.get(i))) {
                return true;
            }
        }
        return false;
    }
}

