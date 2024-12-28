package com.coh.cohBackend.services;

import com.coh.cohBackend.entity.Rank;
import com.coh.cohBackend.entity.User;
import com.coh.cohBackend.repository.*;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class RankService {

    private final RankRepository rankRepository;
    private final JdbcTemplate jdbcTemplate;

    @Autowired
    public RankService(RankRepository rankRepository, JdbcTemplate jdbcTemplate) {
        this.rankRepository = rankRepository;
        this.jdbcTemplate = jdbcTemplate;
    }

    public int saveDefault(User user) {
        Rank rank = new Rank();
        rank.setUserId(user.getUserId());
        rank.setName(user.getUsername());
        rank.setLastLevel("Level 1");
        rank.setTotalTime(0.0);

        return rankRepository.saveAndGetGeneratedId(rank);
    }

    public void updateRank(Rank rank) {

        double totalTime = calculateTotalTime(rank.getUserId());
        rank.setTotalTime(totalTime);

        rankRepository.update(rank);
    }

    private double calculateTotalTime(int userId) {
        String sql = """
            SELECT COALESCE(SUM(s.time), 0) AS total_time
            FROM stats s
            INNER JOIN correct c ON s.stat_id = c.stat_id
            WHERE c.user_id = ?
        """;

        return jdbcTemplate.queryForObject(sql, Double.class, userId);
    }

    public List<Rank> ranking() {
        return rankRepository.getAllRanksSorted();
    }

}
