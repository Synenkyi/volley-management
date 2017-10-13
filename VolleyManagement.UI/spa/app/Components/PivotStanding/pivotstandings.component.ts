import { Component, OnInit, Input, OnDestroy } from '@angular/core';
import { DecimalPipe } from '@angular/common';
import { ISubscription } from 'rxjs/Subscription';

import { StandingsService } from '../../Services/standings.service';
import { PivotStandings } from '../../Models/Pivot/PivotStandings';
import { PivotStandingsGame } from '../../Models/Pivot/PivotStandingsGame';
import { ShortGameResult } from '../../Models/Pivot/ShortGameResult';

@Component({
    selector: 'pivottable',
    templateUrl: './pivotstandings.component.html',
    styleUrls: ['./pivotstandings.component.css']
})

export class PivotStandingsComponent implements OnInit, OnDestroy {

    @Input() pivotId: number;
    pivotStandings: PivotStandings[];
    pivotTable: PivotStandingsGame[][][];

    private subscription: ISubscription;

    constructor(private standingsService: StandingsService) { }

    ngOnInit(): void {
        if (this.pivotId) {
            this.subscription = this.standingsService.getPivotStandings(this.pivotId)
                .subscribe(standings => {
                    this.pivotStandings = standings;
                    this.pivotTable = this.pivotStandings.map(item => this.getPivotTable(item));
                });
        }
    }

    ngOnDestroy() {
        this.subscription.unsubscribe();
    }

    getPivotTable(pivot: PivotStandings): PivotStandingsGame[][] {
        const teamsCount = pivot.TeamsStandings.length;

        const table = Array.apply(null, Array(teamsCount)).map(() => {
            return new Array(teamsCount);
        });

        for (let i = 0; i < teamsCount; i++) {
            for (let j = 0; j < teamsCount; j++) {
                const tableCell = table[i][j];
                if (!tableCell) {
                    if (i === j) {
                        table[i][j] = PivotStandingsGame.getNonPlayableCell();
                    } else {
                        const rowTeamId = pivot.TeamsStandings[i].TeamId;
                        const colTeamId = pivot.TeamsStandings[j].TeamId;
                        const homeGameResult = pivot.GamesStandings.
                            find(game => (game.HomeTeamId === rowTeamId && game.AwayTeamId === colTeamId));
                        const awayGameResult = pivot.GamesStandings.
                            find(game => (game.HomeTeamId === colTeamId && game.AwayTeamId === rowTeamId));

                        if (homeGameResult) {
                            table[i][j] = homeGameResult.clone();
                            table[j][i] = homeGameResult.transposeResult();
                        } else if (awayGameResult) {
                            table[i][j] = awayGameResult.transposeResult();
                            table[j][i] = awayGameResult.clone();
                        }
                    }
                }
            }
        }
        return table;
    }
}
