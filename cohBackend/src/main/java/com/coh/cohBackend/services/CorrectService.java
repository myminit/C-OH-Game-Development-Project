package com.coh.cohBackend.services;

import com.coh.cohBackend.entity.*;
import com.coh.cohBackend.repository.*;
import com.coh.cohBackend.repository.CorrectRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class CorrectService {

    private final CorrectRepository correctRepository;
    private final QuestRepository questRepository;

    @Autowired
    public CorrectService(CorrectRepository correctRepository, QuestRepository questRepository) {
        this.correctRepository = correctRepository;
        this.questRepository = questRepository;
    }

    public void createCorrect(int userId) {

        List<Quest> quests = questRepository.findAll();

        System.out.println(quests);

        for (Quest quest : quests) {
            Correct correct = new Correct();
            correct.setUserId(userId);
            correct.setQuestId(quest.getQuestId());
            correct.setIsCorrect(false);
            correctRepository.save(correct);
        }
    }

}
