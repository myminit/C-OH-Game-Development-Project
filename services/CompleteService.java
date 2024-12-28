package com.coh.cohBackend.services;

import com.coh.cohBackend.entity.Levels;
import com.coh.cohBackend.entity.Rank;
import com.coh.cohBackend.repository.*;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class CompleteService extends BaseService<Levels> {

    private final CompleteRepository completeRepository;
    private final RankRepository rankRepository;
    private final LevelsRepository levelsRepository;

    @Autowired
    public CompleteService(CompleteRepository completeRepository, RankRepository rankRepository, LevelsRepository levelsRepository) {
        this.completeRepository = completeRepository;
        this.rankRepository = rankRepository;
        this.levelsRepository = levelsRepository;
    }

    @Override
    public List<Levels> findByUserId(int userId) {
        return completeRepository.findLevelsByUserId(userId);
    }

    private int extractNumber(String text) {
        return Integer.parseInt(text.replaceAll("[^0-9]", ""));
    }

    @Override
    public boolean updateStatus(int userId, int levelId, boolean complete) {
        int rowsAffected = completeRepository.updateCompletionStatus(userId, levelId, complete);
        Levels level = levelsRepository.findByLevelId(levelId);
        Rank rank = rankRepository.findByUserId(userId);
        int levelNumber = extractNumber(level.getLevelName());
        int lastLevelNumber = extractNumber(rank.getLastLevel());
        if (levelNumber > lastLevelNumber) {
            rankRepository.upLastLevel(level, rank.getRankId());
        }
        return rowsAffected > 0;
    }
}
