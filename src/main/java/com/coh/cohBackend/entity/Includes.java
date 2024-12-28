package com.coh.cohBackend.entity;


import lombok.Data;

@Data
public class Includes {
    private int levelId;
    private int enemyId;

    // Constructors
    public Includes() {}

    public Includes(int levelId, int enemyId) {
        this.levelId = levelId;
        this.enemyId = enemyId;
    }

    // Getters and Setters
    public int getLevelId() { return levelId; }
    public void setLevelId(int levelId) { this.levelId = levelId; }
    public int getEnemyId() { return enemyId; }
    public void setEnemyId(int enemyId) { this.enemyId = enemyId; }
}