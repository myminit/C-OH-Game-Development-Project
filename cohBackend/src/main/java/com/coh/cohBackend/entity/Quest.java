package com.coh.cohBackend.entity;

import lombok.Data;

@Data
public class Quest {
    private int questId;
    private int levelId;
    private String questQ;
    private String questA;

    // Constructors
    public Quest() {}

    public Quest(int questId, int levelId, String questQ, String questA) {
        this.questId = questId;
        this.levelId = levelId;
        this.questQ = questQ;
        this.questA = questA;
    }

    // Getters and Setters
    public int getQuestId() { return questId; }
    public void setQuestId(int questId) { this.questId = questId; }
    public int getLevelId() { return levelId; }
    public void setLevelId(int levelId) { this.levelId = levelId; }
    public String getQuestQ() { return questQ; }
    public void setQuestQ(String questQ) { this.questQ = questQ; }
    public String getQuestA() { return questA; }
    public void setQuestA(String questA) { this.questA = questA; }
}