package com.coh.cohBackend.repository;

import com.coh.cohBackend.entity.Levels;
import com.coh.cohBackend.entity.Rank;
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
public class RankRepository {

    private final JdbcTemplate jdbcTemplate;

    @Autowired
    public RankRepository(JdbcTemplate jdbcTemplate) {
        this.jdbcTemplate = jdbcTemplate;
    }

    public int save(Rank rank) {
        String sql = "INSERT INTO rank_ (user_id, name, last_level, total_time) VALUES (?, ?, ?, ?)";
        return jdbcTemplate.update(sql, rank.getUserId(), rank.getName(), rank.getLastLevel(), rank.getTotalTime());
    }

    public int saveAndGetGeneratedId(Rank rank) {
        String sql = "INSERT INTO rank_ (user_id, name, last_level, total_time) VALUES (?, ?, ?, ?)";

        KeyHolder keyHolder = new GeneratedKeyHolder();
        jdbcTemplate.update(
                connection -> {
                    PreparedStatement ps = connection.prepareStatement(sql, Statement.RETURN_GENERATED_KEYS);
                    ps.setInt(1, rank.getUserId());
                    ps.setString(2, rank.getName());
                    ps.setString(3, rank.getLastLevel());
                    ps.setDouble(4, rank.getTotalTime());
                    return ps;
                },
                keyHolder
        );

        return keyHolder.getKey().intValue();
    }


    public Rank findById(int rankId) {
        String sql = "SELECT * FROM rank_ WHERE rank_id = ?";
        return jdbcTemplate.queryForObject(sql, new RankRowMapper(), rankId);
    }

    public Rank findByUserId(int userId) {
        String sql = "SELECT * FROM rank_ WHERE user_id = ?";
        return jdbcTemplate.queryForObject(sql, new RankRowMapper(), userId);
    }

    public int update(Rank rank) {
        String sql = "UPDATE rank_ SET name = ?, last_level = ?, total_time = ? WHERE rank_id = ?";
        return jdbcTemplate.update(sql, rank.getName(), rank.getLastLevel(), rank.getTotalTime(), rank.getRankId());
    }

    public double totalTime(int rankId) {
        String sql = "SELECT SUM(time) FROM stats WHERE rank_id = ?";
        Double totalTime = jdbcTemplate.queryForObject(sql, Double.class, rankId);

//        if (totalTime == null) {
//            totalTime = 0.0;
//        }

        String updateSql = "UPDATE rank_ SET total_time = ? WHERE rank_id = ?";
        jdbcTemplate.update(updateSql, totalTime, rankId);

        return totalTime;
    }

    public void upLastLevel(Levels level, int rankId) {
        String sql = "UPDATE rank_ SET last_level = ? WHERE rank_id = ?";
        jdbcTemplate.update(sql, level.getLevelName(), rankId);
    }

    public int deleteById(int rankId) {
        String sql = "DELETE FROM rank_ WHERE rank_id = ?";
        return jdbcTemplate.update(sql, rankId);
    }

    private class RankRowMapper implements RowMapper<Rank> {
        @Override
        public Rank mapRow(ResultSet rs, int rowNum) throws SQLException {
            Rank rank = new Rank();
            rank.setRankId(rs.getInt("rank_id"));
            rank.setUserId(rs.getInt("user_id"));
            rank.setName(rs.getString("name"));
            rank.setLastLevel(rs.getString("last_level"));
            rank.setTotalTime(rs.getDouble("total_time"));
            return rank;
        }
    }

    public List<Rank> getAllRanksSorted() {
        String sql = """
        SELECT * 
        FROM rank_
        WHERE total_time != 0.0 
        ORDER BY   
            total_time ASC,
            CAST(REGEXP_SUBSTR(last_level, '[0-9]+') AS UNSIGNED) DESC  
    """;

        return jdbcTemplate.query(sql, new RankRowMapper());
    }

}
