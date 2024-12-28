package com.coh.cohBackend.repository;

import com.coh.cohBackend.entity.Levels;
import org.springframework.stereotype.Repository;

import java.util.List;

@Repository
public class CompleteRepository extends BaseRepository {

    public List<Levels> findLevelsByUserId(int userId) {
        String sql = "SELECT l.level_id, l.level_name, l.description, p.complete " +
                "FROM levels l " +
                "JOIN play p ON l.level_id = p.level_id " +
                "WHERE p.user_id = ?";
        return queryForList(sql, new Object[]{userId}, Levels.class);
    }

    public int updateCompletionStatus(int userId, int levelId, boolean complete) {
        String sql = "UPDATE play SET complete = ? WHERE user_id = ? AND level_id = ?";
        return update(sql, new Object[]{complete, userId, levelId});
    }
}
