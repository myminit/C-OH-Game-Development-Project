package com.coh.cohBackend.repository;

import com.coh.cohBackend.entity.Levels;
import org.springframework.jdbc.core.BeanPropertyRowMapper;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.stereotype.Repository;

import java.util.List;

@Repository
public class LevelsRepository {

    private final JdbcTemplate jdbcTemplate;

    public LevelsRepository(JdbcTemplate jdbcTemplate) {
        this.jdbcTemplate = jdbcTemplate;
    }

    public List<Levels> findAll() {
        String sql = "SELECT * FROM levels";
        return jdbcTemplate.query(sql, new BeanPropertyRowMapper<>(Levels.class));
    }

    public Levels findByLevelId(int levelId) {
        String sql = "SELECT * FROM levels WHERE level_id = ?";
        return jdbcTemplate.queryForObject(sql, new BeanPropertyRowMapper<>(Levels.class), levelId);
    }
}
