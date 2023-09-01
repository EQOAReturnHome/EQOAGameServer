function event_say()
diagOptions = {}
    npcDialogue = "Lord Thex may come across as untrusting, but it is understandably so. He has been betrayed more times than you could imagine. Remain honorable in his presence and you may one day win him over."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end