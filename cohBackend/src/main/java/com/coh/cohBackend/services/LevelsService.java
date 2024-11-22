package com.coh.cohBackend.services;

import com.coh.cohBackend.entity.Levels;
import com.coh.cohBackend.entity.Play;
import com.coh.cohBackend.repository.LevelsRepository;
import com.coh.cohBackend.repository.PlayRepository;
import org.springframework.beans.factory.annotation.Autowired;

import java.util.List;
import java.util.Optional;

public class LevelsService {

    @Autowired
    private LevelsRepository levelsRepository;
    private PlayRepository playRepository;

    public List<Levels> getAllLevels() {
        return levelsRepository.findAll();
    }

}