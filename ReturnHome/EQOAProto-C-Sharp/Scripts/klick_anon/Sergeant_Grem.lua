function  event_say(choice)
diagOptions = {}
    npcDialogue = "The uptick in Clockwork activity has me concerned. Please, I can't be bothered right now. I need to plan."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end