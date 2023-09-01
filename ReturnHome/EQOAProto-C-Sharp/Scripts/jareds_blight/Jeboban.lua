function  event_say(choice)
diagOptions = {}
    npcDialogue = "I get mistaken for a bandit but I really am a man of chance.  The only way I negotiate is with chance.  I'll tell you what.  Flip this coin."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end