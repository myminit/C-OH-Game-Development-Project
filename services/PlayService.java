package com.coh.cohBackend.services;

import com.coh.cohBackend.entity.Levels;
import com.coh.cohBackend.entity.Play;
import com.coh.cohBackend.repository.LevelsRepository;
import com.coh.cohBackend.repository.PlayRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class PlayService extends BaseService<Play> {

    private final LevelsRepository levelsRepository;
    private final PlayRepository playRepository;

    @Autowired
    public PlayService(LevelsRepository levelsRepository, PlayRepository playRepository) {
        this.levelsRepository = levelsRepository;
        this.playRepository = playRepository;
    }

    // Method to create play entries for each level
    public void createPlay(int userId) {
        List<Levels> levels = levelsRepository.findAll();
        for (Levels level : levels) {
            Play play = new Play();
            play.setUserId(userId);
            play.setLevelId(level.getLevelId());
            play.setComplete(level.getLevelId() == 1);  // Complete if it's the first level
            playRepository.save(play);
        }
    }

    // Implementing abstract method from BaseService
    @Override
    public List<Play> findByUserId(int userId) {
        return playRepository.findByUserId(userId);
    }

    // Implementing abstract method from BaseService
    @Override
    public boolean updateStatus(int userId, int levelId, boolean complete) {
        Play play = playRepository.findByUserIdAndLevelId(userId, levelId);
        if (play != null) {
            play.setComplete(complete);
            playRepository.save(play);
            return true;
        }
        return false;
    }
}
