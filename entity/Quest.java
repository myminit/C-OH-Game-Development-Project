package com.coh.cohBackend.entity;

import lombok.Data;

@Data
public class Quest {
    private int questId;
    private int levelId;
    private String questQ;
    private String questA;
    private String spoil;
    private String hint;

    public Quest() {}

    public Quest(int questId, int levelId, String questQ, String questA, String spoil, String hint) {
        this.questId = questId;
        this.levelId = levelId;
        this.questQ = questQ;
        this.questA = questA;
        this.spoil = spoil;
        this.hint = hint;
    }

    public int getQuestId() { return questId; }
    public void setQuestId(int questId) { this.questId = questId; }
    public int getLevelId() { return levelId; }
    public void setLevelId(int levelId) { this.levelId = levelId; }
    public String getQuestQ() { return questQ; }
    public void setQuestQ(String questQ) { this.questQ = questQ; }
    public String getQuestA() { return questA; }
    public void setQuestA(String questA) { this.questA = questA; }
    public String getSpoil() { return spoil; }
    public void setSpoil(String spoil) { this.spoil = spoil; }
    public String getHint() { return hint; }
    public void setHint(String hint) { this.hint = hint; }
}
