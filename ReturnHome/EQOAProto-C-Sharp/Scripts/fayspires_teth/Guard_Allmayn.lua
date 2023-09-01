function event_say()
diagOptions = {}
    npcDialogue = "A recent expedition around Winter's Deep revealed a gathering of bandits in a small fort on the north side of the lake. If you insist on going in that direction, I suggest you bring a company of friends with you."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end