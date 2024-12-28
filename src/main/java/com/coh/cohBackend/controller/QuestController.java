package com.coh.cohBackend.controller;

import com.coh.cohBackend.entity.Quest;
import com.coh.cohBackend.services.CorrectService;
import com.coh.cohBackend.services.QuestService;
import jakarta.servlet.http.HttpSession;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/api/quests")
public class QuestController {

    @Autowired
    private QuestService questService;
    @Autowired
    private CorrectService correctService;

    @GetMapping("/all")
    public List<Quest> getAllQuests() {
        return questService.getAllQuests();
    }

    @GetMapping("/{id}")
    public ResponseEntity<Quest> getQuestById(@PathVariable int id) {
        Quest quest = questService.getQuestById(id);
        if (quest != null) {
            return new ResponseEntity<>(quest, HttpStatus.OK);
        } else {
            return new ResponseEntity<>(HttpStatus.NOT_FOUND);
        }
    }

}
