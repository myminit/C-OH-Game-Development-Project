package com.coh.cohBackend.entity;

import lombok.Data;

@Data
public class Play {
    private int userId;
    private int levelId;
    private boolean complete;

    // Constructors
    public Play() {}

    public Play(int userId, int levelId, boolean complete) {
        this.userId = userId;
        this.levelId = levelId;
        this.complete = complete;
    }

    // Getters and Setters
    public int getUserId() { return userId; }
    public void setUserId(int userId) { this.userId = userId; }
    public int getLevelId() { return levelId; }
    public void setLevelId(int levelId) { this.levelId = levelId; }
    public boolean isComplete() { return complete; }
    public void setComplete(boolean complete) { this.complete = complete; }
}