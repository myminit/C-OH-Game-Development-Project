package com.coh.cohBackend.repository;

import ch.qos.logback.classic.Level;
import org.springframework.stereotype.Repository;
import com.coh.cohBackend.entity.Levels;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.jdbc.core.BeanPropertyRowMapper;
import org.springframework.jdbc.core.JdbcTemplate;
import java.util.List;

@Repository
public class LevelsRepository {
    @Autowired
    private JdbcTemplate jdbcTemplate;

    public List<Levels> findAll() {
        return jdbcTemplate.query("SELECT * FROM levels",
                new BeanPropertyRowMapper<>(Levels.class));
    }


}

