package com.coh.cohBackend.repository;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.stereotype.Repository;
import java.util.List;

import com.coh.cohBackend.entity.*;

@Repository
public class CorrectRepository {

    @Autowired
    private JdbcTemplate jdbcTemplate;

    public int save(Correct correct) {
        return jdbcTemplate.update(
                "INSERT INTO correct (quest_id, user_id, is_correct) VALUES (?, ?, ?)",
                correct.getQuestId(), correct.getUserId(), correct.isIsCorrect()
        );
    }
}
