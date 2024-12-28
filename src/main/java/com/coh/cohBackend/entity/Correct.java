package com.coh.cohBackend.entity;

import lombok.Data;

@Data
public class Correct {
    private int questId;
    private int userId;
    private int statId;
    private boolean isCorrect;

    // Constructors
    public Correct() {}

    public Correct(int questId, int userId, int statId, boolean isCorrect) {
        this.questId = questId;
        this.userId = userId;
        this.statId = statId;
        this.isCorrect = isCorrect;
    }

    // Getters and Setters
    public int getQuestId() { return questId; }
    public void setQuestId(int questId) { this.questId = questId; }
    public int getUserId() { return userId; }
    public void setUserId(int userId) { this.userId = userId; }
    public int getStatId() { return statId; }
    public void setStatId(int statId) { this.statId = statId; }
    public boolean isCorrect() { return isCorrect; }
    public void setCorrect(boolean correct) { isCorrect = correct; }
}
