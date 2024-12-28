package com.coh.cohBackend.repository;

import com.coh.cohBackend.entity.Quest;
import org.springframework.stereotype.Repository;

import java.util.List;

@Repository
public class QuestRepository extends BaseRepository {

    public List<Quest> findAll() {
        String sql = "SELECT * FROM quest";
        return queryForList(sql, new Object[]{}, Quest.class);
    }

    public Quest findById(int questId) {
        String sql = "SELECT * FROM quest WHERE quest_id = ?";
        return queryForObject(sql, new Object[]{questId}, Quest.class);
    }
}
