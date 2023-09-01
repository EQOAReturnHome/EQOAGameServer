function  event_say(choice)
diagOptions = {}
    npcDialogue = "Someone stole our strongboxes last night! If I don't find them, we will lose everything!"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end