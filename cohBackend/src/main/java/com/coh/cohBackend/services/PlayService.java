package com.coh.cohBackend.services;

import com.coh.cohBackend.entity.Levels;
import com.coh.cohBackend.entity.Play;
import com.coh.cohBackend.repository.LevelsRepository;
import com.coh.cohBackend.repository.PlayRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class PlayService {

    private final LevelsRepository levelsRepository;
    private final PlayRepository playRepository;

    @Autowired
    public PlayService(LevelsRepository levelsRepository, PlayRepository playRepository) {
        this.levelsRepository = levelsRepository;
        this.playRepository = playRepository;
    }

    public void createPlay(int userId) {
        List<Levels> levels = levelsRepository.findAll();

        System.out.println(levels);

        for (Levels level : levels) {
            Play play = new Play();
            play.setUserId(userId);
            play.setLevelId(level.getLevelId());
            play.setComplete(false);
            playRepository.save(play);
        }
    }

}
