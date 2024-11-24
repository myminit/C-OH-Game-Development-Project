package com.coh.cohBackend.repository;

import com.coh.cohBackend.entity.Levels;
import org.springframework.stereotype.Repository;

import java.util.List;

@Repository
public class LevelsRepository extends BaseRepository {

    public List<Levels> findAll() {
        String sql = "SELECT * FROM levels";
        return queryForList(sql, new Object[]{}, Levels.class);
    }
}
