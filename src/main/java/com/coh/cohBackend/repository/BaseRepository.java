package com.coh.cohBackend.repository;

import org.springframework.jdbc.core.BeanPropertyRowMapper;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.beans.factory.annotation.Autowired;

import java.util.List;

public abstract class BaseRepository {

    @Autowired
    protected JdbcTemplate jdbcTemplate;

    protected <T> T queryForObject(String sql, Object[] params, Class<T> clazz) {
        return jdbcTemplate.queryForObject(sql, params, new BeanPropertyRowMapper<>(clazz));
    }

    protected <T> List<T> queryForList(String sql, Object[] params, Class<T> clazz) {
        return jdbcTemplate.query(sql, params, new BeanPropertyRowMapper<>(clazz));
    }

    protected int update(String sql, Object[] params) {
        return jdbcTemplate.update(sql, params);
    }
}
