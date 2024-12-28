package com.coh.cohBackend.entity;

import lombok.Data;

@Data
public class Enemy {
    private int enemyId;
    private String enemyName;
    private String type;
    private String enemyDescription;

    public Enemy() {}

    public Enemy(int enemyId, String enemyName, String type, String enemyDescription) {
        this.enemyId = enemyId;
        this.enemyName = enemyName;
        this.type = type;
        this.enemyDescription = enemyDescription;
    }

    public int getEnemyId() { return enemyId; }
    public void setEnemyId(int enemyId) { this.enemyId = enemyId; }
    public String getEnemyName() { return enemyName; }
    public void setEnemyName(String enemyName) { this.enemyName = enemyName; }
    public String getType() { return type; }
    public void setType(String type) { this.type = type; }
    public String getEnemyDescription() { return enemyDescription; }
    public void setEnemyDescription(String enemyDescription) { this.enemyDescription = enemyDescription; }
}