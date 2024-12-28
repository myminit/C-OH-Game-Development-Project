package com.coh.cohBackend.repository;

import com.coh.cohBackend.entity.Play;
import org.springframework.stereotype.Repository;

import java.util.List;

@Repository
public class PlayRepository extends BaseRepository {

    public int save(Play play) {
        String sql = "INSERT INTO play (user_id, level_id, complete) VALUES (?, ?, ?)";
        return update(sql, new Object[]{play.getUserId(), play.getLevelId(), play.isComplete()});
    }

    public List<Play> findByUserId(int userId) {
        String sql = "SELECT * FROM play WHERE user_id = ?";
        return queryForList(sql, new Object[]{userId}, Play.class);
    }

    public Play findByUserIdAndLevelId(int userId, int levelId) {
        String sql = "SELECT * FROM play WHERE user_id = ? AND level_id = ?";
        return queryForObject(sql, new Object[]{userId, levelId}, Play.class);
    }

}
