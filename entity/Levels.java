package com.coh.cohBackend.entity;

import lombok.Data;

@Data
public class Levels {
    private int levelId;
    private String levelName;
    private String description;

    // Constructors
    public Levels() {}

    public Levels(int levelId, String levelName, String description_spoil, String description) {
        this.levelId = levelId;
        this.levelName = levelName;
        this.description = description;
    }

    // Getters and Setters
    public int getLevelId() { return levelId; }
    public void setLevelId(int levelId) { this.levelId = levelId; }
    public String getLevelName() { return levelName; }
    public void setLevelName(String levelName) { this.levelName = levelName; }
    public String getDescription() { return description; }
    public void setDescription(String description) { this.description = description; }
}