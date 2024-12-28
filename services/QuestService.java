package com.coh.cohBackend.services;

import com.coh.cohBackend.entity.Quest;
import com.coh.cohBackend.repository.QuestRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class QuestService {

    @Autowired
    private QuestRepository questRepository;

    public List<Quest> getAllQuests() {
        return questRepository.findAll();
    }

    public Quest getQuestById(int questId) {
        return questRepository.findById(questId);
    }

}
