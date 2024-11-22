//package com.coh.cohBackend.repository;
//
//import org.springframework.beans.factory.annotation.Autowired;
//import org.springframework.jdbc.core.BeanPropertyRowMapper;
//import org.springframework.jdbc.core.JdbcTemplate;
//import org.springframework.stereotype.Repository;
//import java.util.List;
//
//import com.coh.cohBackend.entity.*;
//
//@Repository
//public class EnemyRepository {
//    @Autowired
//    private JdbcTemplate jdbcTemplate;
//
//    public List<Enemy> findAll() {
//        return jdbcTemplate.query("SELECT * FROM enemy",
//                new BeanPropertyRowMapper<>(Enemy.class));
//    }
//
//    public Enemy findById(int enemyId) {
//        return jdbcTemplate.queryForObject(
//                "SELECT * FROM enemy WHERE enemy_id = ?",
//                new Object[]{enemyId},
//                new BeanPropertyRowMapper<>(Enemy.class)
//        );
//    }
//
//    public int save(Enemy enemy) {
//        return jdbcTemplate.update(
//                "INSERT INTO enemy (enemy_name, type, enemy_description) VALUES (?, ?, ?)",
//                enemy.getEnemyName(), enemy.getType(), enemy.getEnemyDescription()
//        );
//    }
//}