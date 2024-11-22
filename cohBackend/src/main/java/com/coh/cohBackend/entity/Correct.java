package com.coh.cohBackend.entity;

import lombok.Data;

@Data
public class Correct {
    private int questId;
    private int userId;
    private boolean isCorrect;

    // Constructors
    public Correct() {}

    public Correct(int questId, int userId, boolean isCorrect) {
        this.questId = questId;
        this.userId = userId;
        this.isCorrect = isCorrect;
    }

    // Getters and Setters
    public int getQuestId() { return questId; }
    public void setQuestId(int questId) { this.questId = questId; }
    public int getUserId() { return userId; }
    public void setUserId(int userId) { this.userId = userId; }
    public boolean isIsCorrect() { return isCorrect; }
    public void setIsCorrect(boolean isCorrect) { this.isCorrect = isCorrect; }
}