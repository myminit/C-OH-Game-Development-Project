package com.coh.cohBackend.repository;

import com.coh.cohBackend.entity.Stats;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.jdbc.core.RowMapper;
import org.springframework.jdbc.support.GeneratedKeyHolder;
import org.springframework.jdbc.support.KeyHolder;
import org.springframework.stereotype.Repository;

import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.List;

@Repository
public class StatsRepository {

    private final JdbcTemplate jdbcTemplate;

    @Autowired
    public StatsRepository(JdbcTemplate jdbcTemplate) {
        this.jdbcTemplate = jdbcTemplate;
    }

    public int save(Stats stats) {
        String sql = "INSERT INTO stats (rank_id, time) VALUES (?, ?)";
        return jdbcTemplate.update(sql, stats.getRankId(), stats.getTime());
    }

    public Stats findById(int statId) {
        String sql = "SELECT * FROM stats WHERE stat_id = ?";
        return jdbcTemplate.queryForObject(sql, new StatsRowMapper(), statId);
    }

    public int update(Stats stats) {
        String sql = "UPDATE stats SET time = ? WHERE stat_id = ?";
        return jdbcTemplate.update(sql, stats.getTime(), stats.getStatId());
    }


    public List<Stats> findByRankId(int rankId) {
        String sql = "SELECT * FROM stats WHERE rank_id = ?";
        return jdbcTemplate.query(sql, new StatsRowMapper(), rankId);
    }

    public int deleteById(int statId) {
        String sql = "DELETE FROM stats WHERE stat_id = ?";
        return jdbcTemplate.update(sql, statId);
    }

    private static class StatsRowMapper implements RowMapper<Stats> {
        @Override
        public Stats mapRow(ResultSet rs, int rowNum) throws SQLException {
            Stats stats = new Stats();
            stats.setStatId(rs.getInt("stat_id"));
            stats.setRankId(rs.getInt("rank_id"));
            stats.setTime(rs.getDouble("time"));
            return stats;
        }
    }

    public int saveAndGetGeneratedId(Stats stats) {
        String sql = "INSERT INTO stats (rank_id, time) VALUES (?, ?)";

        KeyHolder keyHolder = new GeneratedKeyHolder();
        jdbcTemplate.update(
                connection -> {
                    PreparedStatement ps = connection.prepareStatement(sql, Statement.RETURN_GENERATED_KEYS);
                    ps.setInt(1, stats.getRankId());
                    ps.setDouble(2, stats.getTime());
                    return ps;
                },
                keyHolder
        );

        return keyHolder.getKey().intValue();
    }

}
