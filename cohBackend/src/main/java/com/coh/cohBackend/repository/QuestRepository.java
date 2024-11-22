package com.coh.cohBackend.repository;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.jdbc.core.BeanPropertyRowMapper;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.stereotype.Repository;
import java.util.List;

import com.coh.cohBackend.entity.Quest;

@Repository
public class QuestRepository {

    @Autowired
    private JdbcTemplate jdbcTemplate;

    public List<Quest> findAll() {
        return jdbcTemplate.query("SELECT * FROM quest",
                new BeanPropertyRowMapper<>(Quest.class));
    }

}

