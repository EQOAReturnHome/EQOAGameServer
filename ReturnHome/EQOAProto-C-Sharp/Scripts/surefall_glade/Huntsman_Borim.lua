function  event_say(choice)
diagOptions = {}
    npcDialogue = "Be mindful of our laws and do not interfere with our preservation of this wilderness."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end