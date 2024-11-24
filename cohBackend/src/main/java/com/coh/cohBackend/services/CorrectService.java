package com.coh.cohBackend.services;

import com.coh.cohBackend.entity.Correct;
import com.coh.cohBackend.entity.Quest;
import com.coh.cohBackend.repository.CorrectRepository;
import com.coh.cohBackend.repository.QuestRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class CorrectService extends BaseService<Correct> {

    private final CorrectRepository correctRepository;
    private final QuestRepository questRepository;

    @Autowired
    public CorrectService(CorrectRepository correctRepository, QuestRepository questRepository) {
        this.correctRepository = correctRepository;
        this.questRepository = questRepository;
    }

    public void createCorrect(int userId) {
        List<Quest> quests = questRepository.findAll();
        for (Quest quest : quests) {
            Correct correct = new Correct();
            correct.setUserId(userId);
            correct.setQuestId(quest.getQuestId());
            correct.setIsCorrect(false);
            correctRepository.save(correct);
        }
    }

    @Override
    public List<Correct> findByUserId(int userId) {
        return correctRepository.findByUserId(userId);
    }

    @Override
    public boolean updateStatus(int userId, int questId, boolean status) {
        int result = correctRepository.updateIsCorrectToTrue(userId, questId);
        return result > 0;
    }

    public boolean markQuestAsCorrect(int userId, int questId) {
        return updateStatus(userId, questId, true);
    }
}
