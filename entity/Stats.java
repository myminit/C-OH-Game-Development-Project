package com.coh.cohBackend.entity;
import lombok.Data;

@Data
public class Stats {
    private int statId;
    private int rankId;
    private double time;

    // Constructors
    public Stats() {}

    public Stats(int statId, int rankId, double time) {
        this.statId = statId;
        this.rankId = rankId;
        this.time = time;
    }

    // Getters and Setters
    public int getStatId() { return statId; }
    public void setStatId(int statId) { this.statId = statId; }
    public int getRankId() { return rankId; }
    public void setRankId(int rankId) { this.rankId = rankId; }
    public double getTime() { return time; }
    public void setTime(double time) { this.time = time; }
}
