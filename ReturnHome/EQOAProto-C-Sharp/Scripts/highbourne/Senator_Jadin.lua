function event_say()
diagOptions = {}
    npcDialogue = "Colis always seems to vote against whatever I present before the Senate. Take my last bill, it was designed to increase revenue from all of the travelers coming to study at the archives. He says no one visits due to that new bill but does he realize there is more than one gate to this city?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end