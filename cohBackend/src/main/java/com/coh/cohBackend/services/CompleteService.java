package com.coh.cohBackend.services;

import com.coh.cohBackend.entity.Levels;
import com.coh.cohBackend.repository.CompleteRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class CompleteService extends BaseService<Levels> {

    private final CompleteRepository completeRepository;

    @Autowired
    public CompleteService(CompleteRepository completeRepository) {
        this.completeRepository = completeRepository;
    }

    @Override
    public List<Levels> findByUserId(int userId) {
        return completeRepository.findLevelsByUserId(userId);
    }

    @Override
    public boolean updateStatus(int userId, int levelId, boolean complete) {
        int rowsAffected = completeRepository.updateCompletionStatus(userId, levelId, complete);
        return rowsAffected > 0;
    }
}
