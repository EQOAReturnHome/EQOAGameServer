function event_say()
diagOptions = {}
    npcDialogue = "Though we have found refuge here, we vow to never be caught off guard, lest the Teir'Dal decide to strike again. Together with our brethren wood elves in Tethelin, we shall be prepared to defend our cities. Thus, we must train many young paladins in the ways of war."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end