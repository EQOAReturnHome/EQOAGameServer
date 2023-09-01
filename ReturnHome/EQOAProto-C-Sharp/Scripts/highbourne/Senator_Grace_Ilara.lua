function event_say()
diagOptions = {}
    npcDialogue = "I was once a paladin but felt my talents would be of more use if I was elected to the Senate. Here I am and I aim to bring more attention to the paladin guild with the hopes of bolstering the ranks with new recruits."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end