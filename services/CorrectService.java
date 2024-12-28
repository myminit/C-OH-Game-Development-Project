package com.coh.cohBackend.services;

import com.coh.cohBackend.entity.Correct;
import com.coh.cohBackend.entity.Quest;
import com.coh.cohBackend.entity.Stats;
import com.coh.cohBackend.repository.*;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class CorrectService {

    private final CorrectRepository correctRepository;
    private final QuestRepository questRepository;
    private final StatsRepository statsRepository;
    private final RankRepository rankRepository;

    @Autowired
    public CorrectService(CorrectRepository correctRepository, QuestRepository questRepository, StatsRepository statsRepository, RankRepository rankRepository) {
        this.correctRepository = correctRepository;
        this.questRepository = questRepository;
        this.statsRepository = statsRepository;
        this.rankRepository = rankRepository;
    }

    public void createCorrect(int userId, int rankId) {
        List<Quest> quests = questRepository.findAll();
        for (Quest quest : quests) {
            Stats stats = new Stats();
            stats.setRankId(rankId);
            stats.setTime(0.0);
            int statId = statsRepository.saveAndGetGeneratedId(stats);

            Correct correct = new Correct();
            correct.setUserId(userId);
            correct.setQuestId(quest.getQuestId());
            correct.setStatId(statId);
            correct.setCorrect(false);

            correctRepository.save(correct);
        }
    }

    public List<Correct> findByUserId(int userId) {
        return correctRepository.findByUserId(userId);
    }

    public boolean updateStatus(int userId, int questId, int stat_id) {
        int result = correctRepository.updateIsCorrectToTrue(userId, questId, stat_id);
        return result > 0;
    }

    public boolean markQuestAsCorrect(int userId, int questId, Double time) {
        Stats stat = statsRepository.findById(correctRepository.findByUserIdAndQuestId(userId, questId).getStatId());
        boolean isUpdated = updateStatus(userId, questId, stat.getStatId());

        if (isUpdated && statsRepository.findById(stat.getStatId()).getTime() == 0) {
            Correct correct = correctRepository.findByUserIdAndQuestId(userId, questId);
            if (correct != null) {
                Stats stats = statsRepository.findById(correct.getStatId());
                if (stats != null) {
                    stats.setTime(time);
                    statsRepository.update(stats);
                    rankRepository.totalTime(stat.getRankId());
                    return true;
                }
            }
        }
        return true;
    }
}
