function  event_say(choice)
diagOptions = {}
    npcDialogue = "Qeynos may remain a lawful state, but everyone celebrates some tasteful combat from time to time. Go ahead, bring a friend and start a duel. You never know what will happen."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end