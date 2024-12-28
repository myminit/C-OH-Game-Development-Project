package com.coh.cohBackend.entity;

import lombok.Data;

@Data
public class Rank {
    private int rankId;
    private int userId;
    private String name;
    private String lastLevel;
    private double totalTime;

    // Constructors
    public Rank() {}

    public Rank(int rankId, int userId, String name, String lastLevel, double totalTime) {
        this.rankId = rankId;
        this.userId = userId;
        this.name = name;
        this.lastLevel = lastLevel;
        this.totalTime = totalTime;
    }

    // Getters and Setters
    public int getRankId() { return rankId; }
    public void setRankId(int rankId) { this.rankId = rankId; }
    public int getUserId() { return userId; }
    public void setUserId(int userId) { this.userId = userId; }
    public String getName() { return name; }
    public void setName(String name) { this.name = name; }
    public String getLastLevel() { return lastLevel; }
    public void setLastLevel(String lastLevel) { this.lastLevel = lastLevel; }
    public double getTotalTime() { return totalTime; }
    public void setTotalTime(double totalTime) { this.totalTime = totalTime; }
}
