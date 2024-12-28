package com.coh.cohBackend.services;

import com.coh.cohBackend.entity.Levels;
import com.coh.cohBackend.entity.Play;
import com.coh.cohBackend.repository.LevelsRepository;
import com.coh.cohBackend.repository.PlayRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import org.springframework.stereotype.Service;
import java.util.List;

@Service
public class LevelsService {

    private final LevelsRepository levelsRepository;
    private final PlayRepository playRepository;

    public LevelsService(LevelsRepository levelsRepository, PlayRepository playRepository) {
        this.levelsRepository = levelsRepository;
        this.playRepository = playRepository;
    }

    public List<Levels> getAllLevels() {
        return levelsRepository.findAll();
    }
}
