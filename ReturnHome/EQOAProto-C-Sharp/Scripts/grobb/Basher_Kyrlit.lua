function  event_say(choice)
diagOptions = {}
    npcDialogue = "Underlord Solthe does not tolerate any dereliction of duty, playerName. The last initiate did not survive long. We all had to endure his screams for many hours."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end