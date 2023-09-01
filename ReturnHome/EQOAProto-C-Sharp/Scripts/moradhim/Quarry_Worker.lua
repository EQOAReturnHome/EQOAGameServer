function  event_say(choice)
diagOptions = {}
    npcDialogue = "How do they expect me to keep diggin with all these filthy rats about?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end