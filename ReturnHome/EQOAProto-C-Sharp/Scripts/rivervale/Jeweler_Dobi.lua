function  event_say(choice)
diagOptions = {}
    npcDialogue = "I can teach you how to enthrall any who gaze upon your jeweled creations. It will take time and dedication to become respected."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end