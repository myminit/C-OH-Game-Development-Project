package com.coh.cohBackend.repository;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.jdbc.core.RowMapper;
import org.springframework.stereotype.Repository;

import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.List;

import com.coh.cohBackend.entity.Correct;

@Repository
public class CorrectRepository extends BaseRepository {

    @Autowired
    private JdbcTemplate jdbcTemplate;

    public int save(Correct correct) {
        return jdbcTemplate.update(
                "INSERT INTO correct (quest_id, user_id, stat_id, is_correct) VALUES (?, ?, ?, ?)",
                correct.getQuestId(), correct.getUserId(), correct.getStatId(), correct.isCorrect()
        );
    }

    public int updateIsCorrectToTrue(int userId, int questId, int statId) {
        String sql = "UPDATE correct SET is_correct = true WHERE stat_id = ? AND user_id = ? AND quest_id = ?";
        return jdbcTemplate.update(sql, statId, userId, questId);
    }

    public List<Correct> findByUserId(int userId) {
        String sql = "SELECT * FROM correct WHERE user_id = ?";
        return jdbcTemplate.query(sql, new Object[]{userId}, new CorrectRowMapper());
    }

    public Correct findByUserIdAndQuestId(int userId, int questId) {
        String sql = "SELECT * FROM correct WHERE user_id = ? AND quest_id = ?";
        return jdbcTemplate.queryForObject(sql, new Object[]{userId, questId}, new CorrectRowMapper());
    }

    private static class CorrectRowMapper implements RowMapper<Correct> {
        @Override
        public Correct mapRow(ResultSet rs, int rowNum) throws SQLException {
            Correct correct = new Correct();
            correct.setQuestId(rs.getInt("quest_id"));
            correct.setUserId(rs.getInt("user_id"));
            correct.setStatId(rs.getInt("stat_id"));
            correct.setCorrect(rs.getBoolean("is_correct"));
            return correct;
        }
    }
}
